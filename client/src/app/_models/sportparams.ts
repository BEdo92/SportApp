export class SportParams {
    pageNumber = 1;
    pageSize = 10;
    sportType: string | null = null;
    dateFrom: string | null = null;
    dateTo: string | null = null;
    distanceFrom: number | null = null;
    distanceTo: number | null = null;
    orderBy = 'date';
}