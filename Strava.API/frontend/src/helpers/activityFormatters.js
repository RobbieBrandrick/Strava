const activityFormatter = {
  formatTime(data) {
    return `${activityFormatter.formatHours(data)}:${activityFormatter.formatMinutes(data)}:${activityFormatter.formatSeconds(data)}`;
  },
  formatHours(data) {
    return Math.round(Math.floor(((data / 60) / 60)) % 60).toString().padStart(2, '0');
  },
  formatMinutes(data) {
    return Math.round((data / 60) % 60).toString().padStart(2, '0');
  },
  formatSeconds(data) {
    return Math.round(data % 60).toString().padStart(2, '0');
  },
  formatDistance(data) {
    return `${(data / 1000).toFixed(2)}`;
  },
  formatRideSpeed(data) {
    return `${(data * 3.6).toFixed(2).padStart(5, '0')}`;
  },
  formatElevation(data) {
    return `${Math.round(data, 2).toFixed(2).toString().padStart(6, '0')}`;
  },
  formatEfforts(data) {
    return Math.round(data).toString();
  },
  formatRunSpeed(data) {
    if (data === 0) {
      return '-';
    }

    return `${(16.666666667 / data).toFixed(2)}`;
  },
  formatSpeed(data, type) {
    if (type === 'Run') {
      return activityFormatter.formatRunSpeed(data);
    }
    if (type === 'Ride') {
      return activityFormatter.formatRideSpeed(data);
    }
    return data;
  },
  formatActivities(data) {
    const activities = [];

    data.forEach((a) => {
      activities.push({
        id: a.id,
        type: a.type,
        name: a.name,
        localDate: a.localDate,
        utcDate: a.utcDate,
        timezone: a.timezone,
        efforts: 1,
        movingTime: activityFormatter.formatTime(a.movingTime),
        elapsedTime: activityFormatter.formatTime(a.elapsedTime),
        distance: activityFormatter.formatDistance(a.distance),
        averageSpeed: activityFormatter.formatSpeed(a.averageSpeed, a.type),
        maxSpeed: activityFormatter.formatSpeed(a.maxSpeed, a.type),
        elevationGain: activityFormatter.formatElevation(a.elevationGain),
        elevationHigh: activityFormatter.formatElevation(a.elevationHigh),
        elevationLow: activityFormatter.formatElevation(a.elevationLow),
      });
    });
    return activities;
  },
};

export default activityFormatter;
