/**
 * Model cho sự kiện
 */
export class CalendarEvent {
    constructor(
        public eventcalenderId: string,
        public title: string,
        public content: string,
        public start: Date,
        public end: Date,

        public employeeId: string,
        public employeeName: string,

        public approverId: string,
        public approverName: string,

        public groupId: string,
        public backgroundColor: string,
        public textColor: string,

        public currentStatus: number,
        public createdAt: Date,
    ) {

    }

    get id() {
        return this.eventcalenderId;
    }

    get startTime() {
        return this.start;
    }

    get endTime() {
        return this.end;
    }
}