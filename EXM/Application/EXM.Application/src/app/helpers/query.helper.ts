import { IQueryString } from "../interfaces";

export class QueryHelper {
  public static AddQueryStrings(url: string, keyPairValue: Array<IQueryString>): string {
    let counter = 0;

    keyPairValue.forEach((query: IQueryString) => {
      if (query.value && counter == 0) {
        url = `${url}${query.key}=${query.value}`;
        counter++;
      } else if (query.value) {
        url = `${url}&${query.key}=${query.value}`;
      }
    });

    return url;
  }
}
