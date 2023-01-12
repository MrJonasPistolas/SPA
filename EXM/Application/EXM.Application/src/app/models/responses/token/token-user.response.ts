import { RoleResponse } from "../roles";

export interface TokenUserResponse {
  name: string;
  email: string;
  roles: Array<RoleResponse>;
  userPicture: string;
}
