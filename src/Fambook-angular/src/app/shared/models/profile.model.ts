import { Deserializable } from './interfaces/deserializable.model';

export class Profile implements Deserializable {
  id: number;
  gender: string;
  profilePicture: any;
  description: string;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
