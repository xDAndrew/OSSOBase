import Component, { mixins } from 'vue-class-component';
import Template from '@/components/TsoTable/TsoTable.component.vue'
import { DataTableHeader } from 'vuetify';
import axios from 'axios';
import ShortTsoItem from '@/contract/ShortTsoItem';

@Component<TsoTable>({
    components: {},
    render: h => h(Template)
})
export default class TsoTable extends mixins() {

    

    componentHeight: string = "80vh";

    headers: DataTableHeader[] = [{
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
    }]

    tsoList: ShortTsoItem[] = [];
    onlyMine: boolean = false;
    searchResult: string = "";
    noDataResult: string = "Записей не найдено";
    noData: string = "Нет данных";

    itemsPerPage: number = 25;
    page: number = 1;
    pageCount: number = 1;

    created() {
        axios
            .get("api/tso")
            .then(response => {
                this.tsoList = response.data
            })
    }
}