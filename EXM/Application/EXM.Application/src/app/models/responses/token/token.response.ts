import { RoleResponse } from "../roles";

export interface TokenResponse {
  name: string;
  email: string;
  roles: Array<RoleResponse>;
  token: string;
  refreshToken: string;
  userImageURL: string;
  refreshTokenExpiryTime: Date;
}
