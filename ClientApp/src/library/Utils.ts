import { logging } from "protractor";
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
    month: "short",
    day: "numeric",
    hour: "numeric",
    minute: "numeric",
    hour12: true,
  },
};
export default class Utils {
  static dateStyles = dateStyles;
  static dateToString(date: Date, dateStyle) {
    if (date.getFullYear() !== (new Date).getFullYear()) {
      dateStyle['year'] = 'numeric'
    }
    return date.toLocaleTimeString(environment.lang, dateStyle);
  }
}
