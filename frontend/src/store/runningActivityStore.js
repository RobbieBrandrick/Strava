import moment from 'moment';

const runningActivityStore = {
  groupByDate(data, type, key) {
    const result = new Map();

    data.forEach((activity) => {
      const date = activity.localDate.substr(0, 10);
      const dateKey = runningActivityStore.getDateKey(date, type);

      if (!result.has(dateKey)) {
        result.set(dateKey, 0);
      }

      const newDistance = result.get(dateKey) + activity[key];

      result.set(dateKey, newDistance);
    });

    return result;
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
export default runningActivityStore;
