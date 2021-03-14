<template>
  <div>
    <table
      class="table table-hover table-hover table-bordered table-responsive"
    >
      <thead class="table-primary">
        <tr>
          <th v-for="key in columns" :key="key" @click="sortBy(key)">
            {{ key | capitalize }}
            <span class="arrow" :class="sortOrders[key] > 0 ? 'asc' : 'dsc'">
              <svg
                v-if="sortKey == key && sortOrders[key] > 0"
                xmlns="http://www.w3.org/2000/svg"
                width="16"
                height="16"
                fill="currentColor"
                class="bi bi-arrow-down float-right"
                viewBox="0 0 16 16"
              >
                <path
                  fill-rule="evenodd"
                  d="M8 1a.5.5 0 0 1 .5.5v11.793l3.146-3.147a.5.5 0 0 1 .708.708l-4 4a.5.5 0 0 1-.
                  708 0l-4-4a.5.5 0 0 1 .708-.708L7.5 13.293V1.5A.5.5 0 0 1 8 1z"
                />
              </svg>

              <svg
                v-if="sortKey == key && sortOrders[key] < 0"
                xmlns="http://www.w3.org/2000/svg"
                width="16"
                height="16"
                fill="currentColor"
                class="bi bi-arrow-up float-right"
                viewBox="0 0 16 16"
              >
                <path
                  fill-rule="evenodd"
                  d="M8 15a.5.5 0 0 0 .5-.5V2.707l3.146 3.147a.5.5 0 0 0 .708-.708l-4-4a.5.5 0 0 0-.
                  708 0l-4 4a.5.5 0 1 0 .708.708L7.5 2.707V14.5a.5.5 0 0 0 .5.5z"
                />
              </svg>
            </span>
          </th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="entry in filteredData" :key="entry.id">
          <td v-for="key in columns" :key="key">
            {{ entry[key] }}
          </td>
        </tr>
      </tbody>
    </table>
    <nav
      class="d-flex justify-content-center mb-2"
      aria-label="Page navigation example"
    >
      <ul class="pagination">
        <li class="page-item">
          <button
            class="btn btn-primary mr-1"
            aria-label="Previous"
            :disabled="page <= 1"
            @click="updatePage(page - 1)"
          >
            <span aria-hidden="true">&laquo;</span>
            <span class="sr-only">Previous</span>
          </button>
        </li>
        <li
          class="page-item"
          v-for="pageNumber in getPageNumbers"
          :key="pageNumber"
        >
          <button
            class="btn btn-primary mr-1"
            :disabled="page === pageNumber"
            @click="updatePage(pageNumber)"
          >
            {{ pageNumber }}
          </button>
        </li>
        <li class="page-item">
          <button
            class="btn btn-primary mr-1"
            aria-label="Next"
            :disabled="page >= getNumberOfPages"
            @click="updatePage(page + 1)"
          >
            <span aria-hidden="true">&raquo;</span>
            <span class="sr-only">Next</span>
          </button>
        </li>
      </ul>
    </nav>
  </div>
</template>

<script>
export default {
  name: 'GridView',
  props: {
    data: Array,
    columns: Array,
    startPage: {
      type: Number,
      default: 1,
    },
    perPage: {
      type: Number,
      default: 5,
    },
  },
  data() {
    const sortOrders = {};
    this.columns.forEach((key) => {
      sortOrders[key] = 1;
    });
    return {
      sortKey: '',
      sortOrders,
      page: this.startPage,
    };
  },
  computed: {
    filteredData() {
      let { data } = this;

      if (this.sortKey) {
        data = data.slice().sort((a, b) => {
          const lhs = a[this.sortKey];
          const rhs = b[this.sortKey];

          if (lhs === rhs) {
            return 0;
          }
          if (lhs > rhs) {
            return 1;
          }
          return -1;
        });
      }

      data = this.paginate(data);

      return data;
    },
    getPageNumbers() {
      const delta = 3;
      const numberOfPages = this.getNumberOfPages();
      const pages = [];

      for (let i = this.page - delta; i <= this.page + delta; i += 1) {
        if (i >= 1 && i <= numberOfPages) {
          pages.push(i);
        }
      }

      return pages;
    },
  },
  filters: {
    capitalize: (str) => str.charAt(0).toUpperCase() + str.slice(1),
  },
  methods: {
    sortBy(key) {
      this.sortKey = key;
      this.sortOrders[key] *= -1;
    },
    paginate(data) {
      const from = this.page * this.perPage - this.perPage;
      const to = this.page * this.perPage;
      return data.slice(from, to);
    },
    updatePage(value) {
      this.page = value;
    },
    getNumberOfPages() {
      return Math.ceil(this.data.length / this.perPage);
    },
  },
};
</script>
