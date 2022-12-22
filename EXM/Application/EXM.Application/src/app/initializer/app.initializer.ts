import { Observable } from "rxjs";
import {
  Result,
  TokenResponse
} from "../models";
import { AuthService } from "../services";

export function AppInitializer(_AuthService: AuthService): () => Observable<Result<TokenResponse> | null> {
  return () => _AuthService.refreshToken();
}
