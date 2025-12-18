// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

import { Role } from '../enums/role.enum';

export interface User {
  id: string;
  email: string;
  name: string;
  roles: Role[];
  permissions?: string[];
}
