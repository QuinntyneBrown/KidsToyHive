module.exports = {
  name: 'features-security',
  preset: '../../../jest.config.js',
  coverageDirectory: '../../../coverage/libs/features/security',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};
