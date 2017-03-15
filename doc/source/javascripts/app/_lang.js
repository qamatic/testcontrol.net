/*
 *
 *  *
 *  *  * <!--
 *  *  *   ~
 *  *  *   ~ The MIT License (MIT)
 *  *  *   ~
 *  *  *   ~ Copyright (c) 2010-2017 QAMatic
 *  *  *   ~
 *  *  *   ~ Permission is hereby granted, free of charge, to any person obtaining a copy
 *  *  *   ~ of this software and associated documentation files (the "Software"), to deal
 *  *  *   ~ in the Software without restriction, including without limitation the rights
 *  *  *   ~ to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  *  *   ~ copies of the Software, and to permit persons to whom the Software is
 *  *  *   ~ furnished to do so, subject to the following conditions:
 *  *  *   ~
 *  *  *   ~ The above copyright notice and this permission notice shall be included in all
 *  *  *   ~ copies or substantial portions of the Software.
 *  *  *   ~
 *  *  *   ~ THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  *  *   ~ IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  *  *   ~ FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  *  *   ~ AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  *  *   ~ LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  *  *   ~ OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *  *  *   ~ SOFTWARE.
 *  *  *   ~
 *  *  *   ~
 *  *  *   -->
 *  *
 *  *
 *
 */

//= require ../lib/_jquery
(function (global) {
  'use strict';

  var languages = [];

  global.setupLanguages = setupLanguages;
  global.activateLanguage = activateLanguage;

  function activateLanguage(language) {
    if (!language) return;
    if (language === "") return;

    $(".lang-selector a").removeClass('active');
    $(".lang-selector a[data-language-name='" + language + "']").addClass('active');
    for (var i=0; i < languages.length; i++) {
      $(".highlight.tab-" + languages[i]).hide();
      $(".lang-specific." + languages[i]).hide();
    }
    $(".highlight.tab-" + language).show();
    $(".lang-specific." + language).show();

    global.toc.calculateHeights();

    // scroll to the new location of the position
    if ($(window.location.hash).get(0)) {
      $(window.location.hash).get(0).scrollIntoView(true);
    }
  }

  // parseURL and stringifyURL are from https://github.com/sindresorhus/query-string
  // MIT licensed
  // https://github.com/sindresorhus/query-string/blob/7bee64c16f2da1a326579e96977b9227bf6da9e6/license
  function parseURL(str) {
    if (typeof str !== 'string') {
      return {};
    }

    str = str.trim().replace(/^(\?|#|&)/, '');

    if (!str) {
      return {};
    }

    return str.split('&').reduce(function (ret, param) {
      var parts = param.replace(/\+/g, ' ').split('=');
      var key = parts[0];
      var val = parts[1];

      key = decodeURIComponent(key);
      // missing `=` should be `null`:
      // http://w3.org/TR/2012/WD-url-20120524/#collect-url-parameters
      val = val === undefined ? null : decodeURIComponent(val);

      if (!ret.hasOwnProperty(key)) {
        ret[key] = val;
      } else if (Array.isArray(ret[key])) {
        ret[key].push(val);
      } else {
        ret[key] = [ret[key], val];
      }

      return ret;
    }, {});
  };

  function stringifyURL(obj) {
    return obj ? Object.keys(obj).sort().map(function (key) {
      var val = obj[key];

      if (Array.isArray(val)) {
        return val.sort().map(function (val2) {
          return encodeURIComponent(key) + '=' + encodeURIComponent(val2);
        }).join('&');
      }

      return encodeURIComponent(key) + '=' + encodeURIComponent(val);
    }).join('&') : '';
  };

  // gets the language set in the query string
  function getLanguageFromQueryString() {
    if (location.search.length >= 1) {
      var language = parseURL(location.search).language
      if (language) {
        return language;
      } else if (jQuery.inArray(location.search.substr(1), languages) != -1) {
        return location.search.substr(1);
      }
    }

    return false;
  }

  // returns a new query string with the new language in it
  function generateNewQueryString(language) {
    var url = parseURL(location.search);
    if (url.language) {
      url.language = language;
      return stringifyURL(url);
    }
    return language;
  }

  // if a button is clicked, add the state to the history
  function pushURL(language) {
    if (!history) { return; }
    var hash = window.location.hash;
    if (hash) {
      hash = hash.replace(/^#+/, '');
    }
    history.pushState({}, '', '?' + generateNewQueryString(language) + '#' + hash);

    // save language as next default
    localStorage.setItem("language", language);
  }

  function setupLanguages(l) {
    var defaultLanguage = localStorage.getItem("language");

    languages = l;

    var presetLanguage = getLanguageFromQueryString();
    if (presetLanguage) {
      // the language is in the URL, so use that language!
      activateLanguage(presetLanguage);

      localStorage.setItem("language", presetLanguage);
    } else if ((defaultLanguage !== null) && (jQuery.inArray(defaultLanguage, languages) != -1)) {
      // the language was the last selected one saved in localstorage, so use that language!
      activateLanguage(defaultLanguage);
    } else {
      // no language selected, so use the default
      activateLanguage(languages[0]);
    }
  }

  // if we click on a language tab, activate that language
  $(function() {

    $(".tab-sql code").prepend('<b class="langmargin">SQL:</b> </br>');
    $(".tab-shell code").prepend('<b class="langmargin">Shell:</b> </br>');
    $(".tab-java code").prepend('<b class="langmargin">Java:</b> </br>');
    $(".tab-xml code").prepend('<b class="langmargin">XML:</b> </br>');
    $(".tab-csharp code").prepend('<b class="langmargin">C#:</b> </br>');
    $(".lang-selector a").on("click", function() {
      var language = $(this).data("language-name");
      pushURL(language);
      activateLanguage(language);
      return false;
    });
    window.onpopstate = function() {
      activateLanguage(getLanguageFromQueryString());
    };
  });
})(window);
