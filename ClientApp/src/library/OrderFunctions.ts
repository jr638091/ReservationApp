export default class ReservationComparers {
  protected static ReservationComparerByDate = { order: "targetDate" };
  protected static ReservationComparerByRanking = { order: "rating" };
  protected static ReservationComparerByTitle = { order: "title" };

  static orders = [
    {
      label: 'By Date Ascending',
      value: {...ReservationComparers.ReservationComparerByDate}
    },
    {
      label: 'By Date Descending',
      value: {...ReservationComparers.ReservationComparerByDate, descending: true}
    },
    {
      label: 'By Alphabetic Ascending',
      value: {...ReservationComparers.ReservationComparerByTitle}
    },
    {
      label: 'By Alphabetic Descending',
      value: {...ReservationComparers.ReservationComparerByTitle, descending: true}
    },
    {
      label: 'By Rating Ascending',
      value: {...ReservationComparers.ReservationComparerByRanking}
    },
    {
      label: 'By Rating Descending',
      value: {...ReservationComparers.ReservationComparerByRanking, descending: true}
    },
  ]
}
