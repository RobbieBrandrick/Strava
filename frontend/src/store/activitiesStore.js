import axios from 'axios';

const activitiesStore = {
  state: {
    data: [],
    loading: true,
    errored: false,
    types: [],
  },
  loadActivities() {
    if (localStorage.getItem('activities')) {
      const data = JSON.parse(localStorage.getItem('activities'));

      this.state.data.splice(0, this.state.data.length);
      this.state.data.unshift(...data);
    }

    axios.get('https://localhost:5001/Activity', { crossdomain: true })
      .then((response) => {
        this.state.loading = true;
        this.state.errored = false;
        this.state.data.splice(0, this.state.data.length);
        this.state.data.unshift(...response.data);

        localStorage.setItem('activities', JSON.stringify(this.state.data));
      })
      .then(() => {
        const types = this.state.data.map((e) => e.type);
        const uniqueTypes = [...new Set(types)];
        uniqueTypes.push('All');
        this.state.types.slice();
        this.state.types.unshift(...uniqueTypes);
      })
      .catch((error) => {
        console.info(error);
        this.state.errored = true;
      })
      .finally(() => { this.state.loading = false; });
  },

};

export default activitiesStore;

// let runningActivityStore = {
//     groupByDate(data, type, key){

//         let result = new Map();

//         data.forEach((activity) => {

//             const date = activity.localDate.substr(0, 10);
//             const dateKey = runningActivityStore.getDateKey(date, type);

//             if (!result.has(dateKey)) {
//                 result.set(dateKey, 0);
//             }

//             const newDistance =  result.get(dateKey) + activity[key];

//             result.set(dateKey, newDistance);

//         });

//         return result;

//     },
//     getDateKey(date, type){

//         date = moment(date);

//         if (type === 'Day') {
//             return date.format('YYYY-MM-DD');
//         }
//         else if (type === 'Week') {
//             return date.format('YYYY-wo');
//         }
//         else if (type === 'Month') {
//             return date.format('YYYY-MMMM');
//         }
//         else if (type === 'Year') {
//             return date.format('YYYY');
//         }

//     }
// }
