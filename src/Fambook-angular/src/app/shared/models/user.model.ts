import { Deserializable } from './interfaces/deserializable.model';
import { Profile } from './profile.model';

export class User implements Deserializable {
  Id: number;
  Email: string;
  Password: string;
  FirstName: string;
  LastName: string;
  Birthdata: string;
  Profile: Profile;

  getFullName() {
    return this.FirstName + ' ' + this.LastName;
  }

  deserialize(input: any): this {
    Object.assign(this, input);
    this.Profile = new Profile().deserialize(input.profile);
    return this;
  }
}
