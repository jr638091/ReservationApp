export default class ReservationComparers {
  protected static ReservationComparerByDate = { order: "TargetDate" };
  protected static ReservationComparerByRanking = { order: "Rating" };
  protected static ReservationComparerByTitle = { order: "Title" };

  static orders = [
    {
      label: 'By Date Ascending',
      value: {...ReservationComparers.ReservationComparerByDate, descending: false}
    },
    {
      label: 'By Date Descending',
      value: {...ReservationComparers.ReservationComparerByDate, descending: true}
    },
    {
      label: 'By Alphabetic Ascending',
      value: {...ReservationComparers.ReservationComparerByTitle, descending: false}
    },
    {
      label: 'By Alphabetic Descending',
      value: {...ReservationComparers.ReservationComparerByTitle, descending: true}
    },
    {
      label: 'By Rating Ascending',
      value: {...ReservationComparers.ReservationComparerByRanking, descending: false}
    },
    {
      label: 'By Rating Descending',
      value: {...ReservationComparers.ReservationComparerByRanking, descending: true}
    },
  ]
}
