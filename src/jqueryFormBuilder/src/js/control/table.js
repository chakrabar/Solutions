import control from '../control';

/**
 * Table - show array data in tabular form
 * Output a <table ... /> html element
 */
export default class controlTable extends control { // arghya
  /**
   * definition
   * @return {Object} select control definition
   */
  static get definition() {
    return {
      icon: '▦',
      i18n: {
        default: 'Table'
      }
    };
  }

  /**
  * build a hidden input dom element
  * @return {Object} DOM Element to be injected into the form.
  */
  build() {
    this.dom = this.markup('div', null, { className: 'table table-striped table-sm' })
    return this.dom;
  }

   /**
   * onRender callback
   */
  onRender() {
    let data = this.config.value;
    console.log(data);
    if (typeof data == 'undefined' || data == null)
        return;
    if (typeof data == 'string') {
        data = data.replace(/'/g, '"'); // if JSON with single-quote, change to double-quote // TODO: can break strings with single quote
        data = JSON.parse(data);
    }        
    let tableHtml = '';
    if (data != null && Array.isArray(data)) {
        const firstLetterUpper = str => str.substring(0, 1).toUpperCase() + str.substring(1);
        tableHtml = '<table class="table">';
        const firstRow = data[0];
        let headers = Object.keys(firstRow).map(col => `<th>${firstLetterUpper(col)}</th>`); // To fix: Headers are based on first row data
        tableHtml += `<thead><tr>${headers.join('')}</tr></thead><tbody>`;
        for (let row of data) {
            let rowHtml = '<tr>';
            for (let column in row) {
                if (row.hasOwnProperty(column)) {
                    rowHtml += '<td>' + row[column] + '</td>';
                }                
            }
            rowHtml += '</tr>';
            tableHtml += rowHtml;
        }
        tableHtml += '</tbody></table>';
    }
    const targetDiv = `div.field-${this.config.name} > div.table`; // '#'+this.config.name+' > div.table';
    $(targetDiv).html(tableHtml); // NOTE: this depends on auto-generated class names
  }
}

// register the following controls
control.register('table', controlTable);
control.register('table', controlTable, 'table');