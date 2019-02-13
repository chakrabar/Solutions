import controlTable from './table';

/**
 * Table - show array data in tabular form
 * Output a <table ... /> html element BUT withou columns - all data in columns in same row
 */
export default class controlNocolumn extends controlTable { // arghya
  /**
   * definition
   * @return {Object} select control definition
   */
  static get definition() {
    return {
      icon: '▦',
      i18n: {
        default: 'No-column'
      }
    };
  }

  /**
  * build a hidden input dom element
  * @return {Object} DOM Element to be injected into the form.
  *
  build() {
    this.dom = this.markup('div', null, { className: 'table table-striped table-sm' })
    return this.dom;
  }*/

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
      tableHtml += '<table class="table">';
      for (let row of data) {
          let rowHtml = '<tr><td>&nbsp;';
          let columns = Object.keys(row).map(col => `<strong>${col}</strong>:${row[col]}`);
          rowHtml += columns.join(', ');
          rowHtml += '&nbsp;</td></tr>';
          tableHtml += rowHtml;
      }
      tableHtml += '</table>';
    }
    const targetDiv = `div.field-${this.config.name} > div.table`; // '#'+this.config.name+' > div.table';
    $(targetDiv).html(tableHtml); // NOTE: this depends on auto-generated class names
  }
}

// register as subtype of table
console.log('registering controlNoColumnTable');
controlTable.register('nocolumn', controlNocolumn, 'table');