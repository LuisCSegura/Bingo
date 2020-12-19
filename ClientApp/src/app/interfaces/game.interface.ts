export interface Game{
    id: number;
    name: string;
    startTime: Date;
    link: string;
    playersNumber: number;
    gettedNumbers: number[];
    finished: boolean;
}