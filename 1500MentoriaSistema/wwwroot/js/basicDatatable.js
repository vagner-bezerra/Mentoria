class basicDataTableGenerate {
    constructor(tableSelector) {
        this.tableSelector = tableSelector;
        this.table = null;
        this.initialize();
    }

    initialize() {
        this.table = $(this.tableSelector).DataTable({
            language: {
                url: '//cdn.datatables.net/plug-ins/1.13.4/i18n/pt-BR.json',
                lengthMenu:
                    '<select class="page-link" style="padding: 0px !important; border-radius: 6px; text-align: center; width: 60px; height: 35px">' +
                    '<option value="10">10</option>' +
                    '<option value="25">25</option>' +
                    '<option value="50">50</option>' +
                    '<option value="-1">Todos</option>' +
                    '</select>',
                search: '',
            },
            responsive: {
                details: false,
            },
            columnDefs: [
                { responsivePriority: 1, targets: [0, 1, -1, -2, -3] },
            ],
            order: [[1, 'asc']],
            dom:
                "<'col-12 px-2 py-2'f>" +
                "<'row'<'col-12'tr>>" +
                "<'row px-2 py-2'<'col-2'l><'col-8'><'col-2'p>>",
            initComplete: this.initializeFilters.bind(this),
        });
    }

    initializeFilters() {
        const count = this.table.columns().count();

        this.table.columns().every((index) => {
            const column = this.table.column(index);
            const select = $('<select class="selectpicker" multiple data-selected-text-format="count"></select>')
                .appendTo($(column.footer()).empty())
                .on('change', () => {
                    if (!$(this).data('isChanging')) {
                        $(this).data('isChanging', true);

                        const selectedValues = $(this).val();
                        // Call the handler method passing the column and select element
                        this.handler(column, select);

                        $(this).data('isChanging', false);
                    }
                });

            select.append('<option value="">Limpar Filtro</option>');

            column.data().unique().sort().each(function (d, j) {
                select.append('<option value="' + d + '">' + d + '</option>');
            });

            select.selectpicker({
                noneSelectedText: "Nenhum filtro selecionado",
                countSelectedText: function (numSelected, numTotal) {
                    if (numSelected > 1) {
                        return "{0} itens selecionados";
                    }
                }
            });
        });

        this.restoreFilters();
    }

    handler(column, el) {
        const values = $(el).val();
        const strings = [];
        values.forEach((x) => {
            const val = $.fn.dataTable.util.escapeRegex(x);
            strings.push(val ? '^' + val + '$' : '');
        });
        column.search(strings.join('|'), true, false).draw();

        const filterValues = JSON.stringify(values);
        sessionStorage.setItem(column.index(), filterValues);
    }

    restoreFilters() {
        this.table.columns().every(function () {
            const column = this;
            const filterValues = sessionStorage.getItem(column.index());
            if (filterValues) {
                const values = JSON.parse(filterValues);
                $(column.footer()).find('.selectpicker').val(values);
                $(column.footer()).find('.selectpicker').selectpicker('refresh');
                this.handler(column, $(column.footer()).find('.selectpicker'));
            }
        });
    }

    isNullOrInvalid(val) {
        return val == undefined || val == null || val == "" || val.length <= 0;
    }
}