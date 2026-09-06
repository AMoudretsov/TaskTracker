export interface PagingState {
  limit: number;
  cursor: number | null;
  hasMore: boolean;
}
