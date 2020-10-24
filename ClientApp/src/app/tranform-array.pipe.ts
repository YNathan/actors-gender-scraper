import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'tranformArray'
})
export class TranformArrayPipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {

    return value.join(" | ");
  }

}
