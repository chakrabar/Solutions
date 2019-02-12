/**
 * Star rating class - show 5 stars with the ability to select a rating
 */

// configure the class for runtime loading
if (!window.fbControls) window.fbControls = [];
window.fbControls.push(function(controlClass) {
  /**
   * Star rating class
   */
  class controlTwoDivs extends controlClass {

    /**
     * Class configuration - return the icons & label related to this control
     * @returndefinition object
     */
    static get definition() {
      return {
        icon: 'U+25EB',
        i18n: {
          default: 'Two Divs'
        }
      };
    }

    /**
     * javascript & css to load
     // 
    configure() {
      this.js = '//cdnjs.cloudflare.com/ajax/libs/rateYo/2.2.0/jquery.rateyo.min.js';
      this.css = '//cdnjs.cloudflare.com/ajax/libs/rateYo/2.2.0/jquery.rateyo.min.css';
    }*/

    /**
     * build a text DOM element, supporting other jquery text form-control's
     * @return {Object} DOM Element to be injected into the form.
     */
    build() {
      return this.markup('div', null, {id: this.config.name});
    }

    /**
     * onRender callback
     */
    onRender() {
      const divs = '<div class="col-sm-6" style="border-right:1px;background-color:red;"></div><div class="col-sm-6" style="background-color:blue"></div>';
      $('#'+this.config.name).append(divs);
    }
  }

  // register this control for the following types & text subtypes
  controlClass.register('twoDivs', controlTwoDivs);
  return controlTwoDivs;
});
