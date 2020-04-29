import { Deserializable } from './interfaces/deserializable.model';

export class Credentials implements Deserializable {
  id: number;
  email: string;
  password: string;
  userId: number;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
