import moment from 'moment';

const activityChartHelper = {
  groupByDate(data, type, column, fillInDates) {
    const result = new Map();

    if (fillInDates) {
      activityChartHelper.applyFillInDates(result, data, type);
    }

    const averageColumns = ['maxSpeed', 'averageSpeed'];

    if (averageColumns.includes(column)) {
      activityChartHelper.groupByDateAvg(result, data, type, column);
    } else {
      activityChartHelper.groupByDateSum(result, data, type, column);
    }
    return result;
  },
  groupByDateSum(result, data, type, column) {
    data.forEach((activity) => {
      const date = activity.localDate.substr(0, 10);
      const dateKey = activityChartHelper.getDateKey(date, type);

      if (!result.has(dateKey)) {
        result.set(dateKey, { data: 0, dates: [] });
      }

      const record = result.get(dateKey);

      record.data += activity[column];
      record.dates.push(date);

      result.set(dateKey, record);
    });

    return result;
  },
  groupByDateAvg(result, data, type, column) {
    data.forEach((activity) => {
      const date = activity.localDate.substr(0, 10);
      const dateKey = activityChartHelper.getDateKey(date, type);

      if (!result.has(dateKey)) {
        result.set(dateKey, { data: activity[column], dates: [] });
      }

      const record = result.get(dateKey);

      record.data = ((record.data + activity[column]) / 2);
      record.dates.push(date);

      result.set(dateKey, record);
    });

    return result;
  },
  applyFillInDates(result, data, type) {
    const dates = data.map((e) => e.localDate.substr(0, 10));
    const startDate = moment(dates.sort((lhs, rhs) => lhs > rhs)[0]);
    const endDate = moment(dates.sort((lhs, rhs) => lhs < rhs)[0]);
    let incrementDateBy;

    if (type === 'Day') {
      incrementDateBy = 'days';
    } else if (type === 'Week') {
      incrementDateBy = 'weeks';
    } else if (type === 'Month') {
      incrementDateBy = 'months';
    } else if (type === 'Year') {
      incrementDateBy = 'years';
    }

    while (endDate.diff(startDate, incrementDateBy) > -1) {
      const dateKey = activityChartHelper.getDateKey(endDate, type);
      if (!result.has(dateKey)) {
        result.set(dateKey, { data: 0, dates: [] });
      }
      endDate.add(-1, incrementDateBy);
    }
  },
  getDateKey(dateStr, type) {
    const date = moment(dateStr);

    if (type === 'Day') {
      return date.format('YYYY-MM-DD');
    } if (type === 'Week') {
      return date.format('YYYY-wo');
    } if (type === 'Month') {
      return date.format('YYYY-MMMM');
    } if (type === 'Year') {
      return date.format('YYYY');
    }

    return null;
  },
};

export default activityChartHelper;
