import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'genderFromRoles'
})
export class GenderFromRolesPipe implements PipeTransform {

  transform(values: string[], ...args: any[]): any {

    for (let role of values) {
    role = role.replace(/\n/,'');
      if (role.toLowerCase() === "actor") {
        return "MALE";
      } else if (role.toLowerCase() === "actress") {
        return "FEMALE";
      }
    }
    return null;
  }

}
