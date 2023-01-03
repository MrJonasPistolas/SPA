import { Role } from "../../../enums";

export interface RoleResponse {
  roleName: Role;
  roleDescription: string;
  selected: boolean;
}
