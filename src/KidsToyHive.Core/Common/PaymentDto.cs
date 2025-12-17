// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.


namespace KidsToyHive.Core.Common;

public class PaymentDto
{
    public string Number { get; set; }
    public long? ExpMonth { get; set; }
    public int ExpYear { get; set; }
    public string Cvc { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }
    public string Currency { get; set; }
}

