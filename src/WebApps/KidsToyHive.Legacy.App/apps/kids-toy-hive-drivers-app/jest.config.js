// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

module.exports = {
  name: 'kids-toy-hive-drivers-app',
  preset: '../../jest.config.js',
  coverageDirectory: '../../coverage/apps/kids-toy-hive-drivers-app',
  snapshotSerializers: [
    'jest-preset-angular/AngularSnapshotSerializer.js',
    'jest-preset-angular/HTMLCommentSerializer.js'
  ]
};

