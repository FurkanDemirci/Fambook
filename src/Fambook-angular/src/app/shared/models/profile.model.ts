import { Deserializable } from './interfaces/deserializable.model';

export class Profile implements Deserializable {
  Id: string;
  Gender: string;
  ProfilePicture: File;
  Description: string;

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
