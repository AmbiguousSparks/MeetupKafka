import { User } from "./user";

export interface TokenResponse {
  token: string;
  validFrom: Date;
  validTo: Date;
  user: User;
}
