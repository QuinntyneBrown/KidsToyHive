module.exports = {
  name: 'kids-toy-hive-admin',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/kids-toy-hive-admin',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
