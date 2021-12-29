import { CalendarOptions } from "@fullcalendar/angular";
import { CalendarService } from "src/app/services/serverHttp/calendar.service";

export const CONFIG_CALENDAR: CalendarOptions = {
    locale: "vi",
    height: "100%",
    allDaySlot: false,
    editable: true,

    headerToolbar: {
        start: "myCustomButton",
        center: 'title',
        end: 'today prev,next'
    },

    dayMaxEventRows: 3,

    titleFormat: {
        year: 'numeric',
        month: '2-digit'
    },
    slotDuration: "00:15:00",

    businessHours: {
        // các ngày trong tuần. một mảng các số nguyên ngày trong tuần dựa trên 0 (0 = Chủ nhật)
        daysOfWeek: [1, 2, 3, 4, 5, 6], // Monday - Thursday
        startTime: '08:00', // a start time (10am in this example)
        endTime: '18:00', // an end time (6pm in this example)
    },

    slotLabelFormat: {
        hour: 'numeric',
        minute: '2-digit',
        omitZeroMinute: false,
        meridiem: 'short',
        hour12: false,
    },
};

export class BaseCalendarFuncion {
    constructor(protected calendarService: CalendarService) { }

}