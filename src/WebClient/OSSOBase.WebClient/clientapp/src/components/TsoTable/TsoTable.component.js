import { __decorate } from "tslib";
import Component, { mixins } from 'vue-class-component';
import Template from '@/components/TsoTable/TsoTable.component.vue';
import axios from 'axios';
let TsoTable = class TsoTable extends mixins() {
    constructor() {
        super(...arguments);
        this.componentHeight = "80vh";
        this.headers = [{
                text: 'Заказчик',
                sortable: true,
                value: 'owner',
                divider: true,
                width: "25%",
                filterable: true,
                align: 'start'
            },
            {
                text: 'Объект',
                sortable: true,
                value: 'objectName',
                divider: true,
                width: "25%",
                filterable: true,
                align: 'start'
            },
            {
                text: 'Адрес',
                sortable: true,
                value: 'address',
                divider: true,
                width: "25%",
                filterable: true,
                align: 'start'
            },
            {
                text: 'У.У.',
                sortable: false,
                value: 'uuAmount',
                divider: true,
                width: "5%",
                filterable: false,
                align: 'start'
            },
            {
                text: 'Пользователь',
                sortable: false,
                value: 'user',
                divider: true,
                width: "15%",
                filterable: false,
                align: 'start'
            },
            {
                text: 'Ред.',
                sortable: false,
                value: 'action',
                divider: true,
                filterable: false,
                width: "5%"
            }];
        this.tsoList = [];
        this.onlyMine = false;
        this.searchResult = "";
        this.noDataResult = "Записей не найдено";
        this.noData = "Нет данных";
        this.itemsPerPage = 25;
        this.page = 1;
        this.pageCount = 1;
    }
    created() {
        axios
            .get("api/tso")
            .then(response => {
            this.tsoList = response.data;
        });
    }
};
TsoTable = __decorate([
    Component({
        components: {},
        render: h => h(Template)
    })
], TsoTable);
export default TsoTable;
//# sourceMappingURL=TsoTable.component.js.map