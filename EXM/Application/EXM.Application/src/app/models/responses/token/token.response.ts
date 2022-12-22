import { RoleResponse } from "../roles";

export interface TokenResponse {
  email: string;
  roles: Array<RoleResponse>;
  token: string;
  refreshToken: string;
  userImageURL: string;
  refreshTokenExpiryTime: Date;
}
