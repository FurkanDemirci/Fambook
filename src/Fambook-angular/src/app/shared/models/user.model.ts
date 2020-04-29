import { Deserializable } from './interfaces/deserializable.model';
import { Profile } from './profile.model';

export class User implements Deserializable {
  id: number;
  firstName: string;
  lastName: string;
  birthdata: string;
  profile: Profile;

  getFullName() {
    return this.firstName + ' ' + this.lastName;
  }

  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
