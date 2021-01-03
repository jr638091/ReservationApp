interface Pagination< T >{
  hits: Number
  count: Number
  initial: Number
  results: Array<T>
}
