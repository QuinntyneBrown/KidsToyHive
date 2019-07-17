module.exports = {
  name: 'domain',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/libs/domain',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
