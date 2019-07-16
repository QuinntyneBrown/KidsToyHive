module.exports = {
  name: 'kids-toy-hive',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/kids-toy-hive',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
