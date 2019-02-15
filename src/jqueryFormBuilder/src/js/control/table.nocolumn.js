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
      icon: '▦', // todod: this icon may not be required
      i18n: {
        default: 'No-column' // this text would show up in sub-type dropdown
      }
    };
  }

  /**
  * onRender callback
  */
  onRender() {
    if (typeof this.config.value == 'undefined' || this.config.value == null)
      return;
    console.log(this.config.value);
    
    let arr = null; // prepare data
    if (typeof this.config.value == 'string') { // we expect an array, if it's string parse it (assuming a json string)
      let str = (' ' + this.config.value).slice(1); // making a copy
      str = str.replace(/'/g, '"'); // if JSON with single-quote, change to double-quote // TODO: can break strings with single quote
      arr = JSON.parse(str);
    } else {
      arr = this.config.value;
    }
    
    if (arr != null && Array.isArray(arr) && arr.length > 0) {
      const data = $.extend(true, [], arr); // make a copy of data before using
      let tableHtml = '<table class="table">';
      for (let row of data) {
          let rowHtml = '<tr><td>&nbsp;';
          let columns = Object.keys(row).map(col => `<strong>${col}</strong>:${row[col]}`); // todo: does not handle complex objects in column
          rowHtml += columns.join(', ');
          rowHtml += '&nbsp;</td></tr>';
          tableHtml += rowHtml;
      }
      tableHtml += '</table>';

      const targetDiv = `div.field-${this.config.name} > div.table`; // '#'+this.config.name+' > div.table';
      $(targetDiv).html(tableHtml); // NOTE: this depends on auto-generated class names
    }    
  }
}

// register as subtype of table
controlTable.register('nocolumn', controlNocolumn, 'table');