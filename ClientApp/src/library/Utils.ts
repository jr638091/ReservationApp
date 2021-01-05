
import { environment } from "src/environments/environment";

const dateStyles = {
  long: {
    weekday: "long",
    month: "short",
    day: "numeric",
    hour: "numeric",
    minute: "numeric",
    hour12: true,
  },
  medium: {
    month: "long",
    day: "numeric",
    hour: "numeric",
    minute: "numeric",
    hour12: true,
  },
  short: {
    year: 'numeric',
    month: "short",
    day: "numeric"
  },
};
export default class Utils {
  static dateStyles = dateStyles;
  static dateToString(date: Date, dateStyle) {
    if (date.getFullYear() !== (new Date).getFullYear()) {
      dateStyle['year'] = 'numeric'
    }
    return date.toLocaleDateString(environment.lang, dateStyle);
  }
}
