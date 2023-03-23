module.exports = {
  name: 'kids-toy-hive-drivers-app',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/kids-toy-hive-drivers-app',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
