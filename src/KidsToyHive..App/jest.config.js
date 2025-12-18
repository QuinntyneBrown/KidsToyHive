// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

module.exports = {
  preset: 'jest-preset-angular',
  setupFilesAfterEnv: ['<rootDir>/setup-jest.ts'],
  testMatch: ['**/+(*.)+(spec|test).+(ts|js)?(x)'],
  transform: {
    '^.+\\.(ts|js|html)$': ['jest-preset-angular', {
      tsconfig: '<rootDir>/tsconfig.spec.json',
      stringifyContentPathRegex: '\\.(html|svg)$',
    }]
  },
  moduleFileExtensions: ['ts', 'js', 'html'],
  collectCoverage: true,
  coverageReporters: ['html', 'text'],
  coveragePathIgnorePatterns: [
    '/node_modules/',
    '/test/',
    '.*\\.spec\\.ts$'
  ],
  testEnvironment: 'jsdom',
  moduleNameMapper: {
    '^@app/(.*)$': '<rootDir>/projects/kids-toy-hive-admin/src/app/$1',
    '^@core/(.*)$': '<rootDir>/projects/kids-toy-hive-admin/src/app/core/$1',
    '^@environments/(.*)$': '<rootDir>/projects/kids-toy-hive-admin/src/environments/$1'
  }
};

