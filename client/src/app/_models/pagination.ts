export interface Pagination {
    currentPage: number;
    itemsPage: number;
    totalItems: number;
    totalPages: number;
}

export class PaginatedResult<T> {
    items?: T | undefined;
    pagination: Pagination | undefined;
}