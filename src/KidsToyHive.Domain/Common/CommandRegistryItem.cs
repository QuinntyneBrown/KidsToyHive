using KidsToyHive.Core;
using KidsToyHive.Core.Common;
using KidsToyHive.Core.Enums;
using KidsToyHive.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace KidsToyHive.Domain
{
    public class CommandRegistryItem
    {
        public string CorrelationId { get; set; } = IdGenerator.GetNextId();
        public string Request { get; set; }
        public string OperationId { get; set; }
        public string RequestDotNetType { get; set; }           
        public string PartitionKey { get; set; }
        public string Key { get; set; }
        public string SideEffects { get; set; }

        private CommandRegistryItemState _state = CommandRegistryItemState.Sleeping;
        public CommandRegistryItemState State {
            get
            { return _state; }
            set
            {                
                _state = value;
                StateChanges.OnNext(value);
            }
        }

        public BehaviorSubject<CommandRegistryItemState> StateChanges { get; private set; } = new BehaviorSubject<CommandRegistryItemState>(CommandRegistryItemState.Sleeping);

        public string ConflictingIds { get; set; }
        public string Result { get; set; }
        public int Index { get; set; }

        public void SetConflictingIds(string conflictingIds)
        {            
            ConflictingIds = conflictingIds;
        }
        public void Cancel()
        {
            State = CommandRegistryItemState.Cancelled;
        }

        public void Complete()
        {
            State = CommandRegistryItemState.Completed;
        }

        public void Error()
        {
            State = CommandRegistryItemState.Failed;
        }

        public void Run()
        {
            State = CommandRegistryItemState.Activated;
        }

        public bool ConflictsWith(CommandRegistryItem request)
            => request.SideEffects.Split(',')
                .Intersect(SideEffects.Split(',')).Any()
                && !string.IsNullOrEmpty(SideEffects);

        public async static Task<CommandRegistryItem> ParseAsync(HttpRequest httpRequest, CancellationToken token = default)
        {
            var body = await new StreamReader(httpRequest.Body).ReadToEndAsync();
            httpRequest.Headers.TryGetValue(Strings.PartitionKey, out StringValues partitionKey);
            httpRequest.Headers.TryGetValue(Strings.OperationId, out StringValues operationId);            
            dynamic request = JsonConvert.DeserializeObject(body, Type.GetType(DotNetTypeMapper.Map(operationId)));
            return Parse(request, operationId, partitionKey, token);
        }

        public CancellationToken CancellationToken { get; set; }
        public static CommandRegistryItem Parse(dynamic request, string operationId, string partitionKey, CancellationToken token = default)
        {
            var item = new CommandRegistryItem
            {
                OperationId = $"{operationId}",
                PartitionKey = partitionKey,
                Request = JsonConvert.SerializeObject(request),
                Key = request.Key,
                SideEffects = string.Join(",", request.SideEffects),
                RequestDotNetType = request.GetType().AssemblyQualifiedName,
                CancellationToken = token
            };
            return item;
        }
        public bool HasConflicts()
            => !string.IsNullOrEmpty(ConflictingIds);

        public IObservable<CommandRegistryItemState> Completed => StateChanges
         .Where(x => x >= CommandRegistryItemState.Completed)
         .Take(1);
    }
}
