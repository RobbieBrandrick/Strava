<template>
  <div class="card">
    <div class="card-header" @click="show = !show">Modify Activities</div>
    <div class="card-body" v-show="show">
      <div class="form-row">
        <div class="form-group col-md-3">
          <label for="activityType">Type</label>
          <select id="activityType" class="form-control" v-model="selectedType">
            <option :selected="true">All</option>
            <option v-for="type in types" :key="type" v-bind:value="type">
              {{ type }}
            </option>
          </select>
        </div>
      </div>

      <div class="form-group col-md-3">
        <button class="btn btn-primary" @click="AddActivity">
          Add Activity
        </button>
      </div>

      <div class="form-group col-md-3">
        <button class="btn btn-primary" @click="RemoveActivity">
          Remove Activity
        </button>
      </div>

      <div class="form-group col-md-3">
        <button class="btn btn-primary" @click="RefreshActivities">
          Refresh Activities
        </button>
      </div>
    </div>
  </div>
</template>

<script>
import getActivitiesMixin from '@/dashboard/components/activities-mixin';

export default {
  name: 'ModifyActivities',
  mixins: [getActivitiesMixin],
  data() {
    return {
      selectedType: 'Run',
      show: false,
    };
  },
  methods: {
    AddActivity() {
      const activity = JSON.parse(JSON.stringify(this.$store.state.activities.data.filter((e) => e.type === this.selectedType)[0]));
      activity.id += '1';
      this.$store.commit('activities/addActivities', [activity]);
    },
    RemoveActivity() {
      const activity = this.$store.state.activities.data.filter((e) => e.type === this.selectedType)[0];
      this.$store.commit('activities/removeActivities', [activity]);
    },
    RefreshActivities() {
      this.refreshActivities();
    },
  },
};
</script>
