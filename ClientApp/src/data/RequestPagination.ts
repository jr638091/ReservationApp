interface Pagination< T >{
  hits: number
  count: number
  pageIndex: number
  nextPage: string
  previousPage: string
  results: Array<T>
}
