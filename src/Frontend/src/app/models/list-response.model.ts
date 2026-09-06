export interface PagingResponse {
  limit: number;
  cursor: number | null | undefined;
  hasMore: boolean;
}

export interface ListResponse<TItem> {
  items: TItem[];
  paging: PagingResponse;
}
