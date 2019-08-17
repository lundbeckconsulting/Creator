const doLOG = entry => console.log(entry);
const doINFO = entry => console.info(entry);
const doWARN = entry => console.warning(entry);
const doERROR = entry => console.error(entry);
const doTABLE = entry => console.table(entry);

const Trim = str => str.trimStart().trimEnd();

const IsNull = obj => obj === undefined || obj === null;

const IsJSON = str => {
  var result = true;

  try {
    JSON.parse(str);
  } catch (e) {
    result = false;
  }

  return result;
}

const equal = (val, compare) => val.toUpperCase() === compare.toUpperCase();

const equalList = (val, ...compares) => {
  var result = false;

  compares.map((compare) => {
    if (equal(val, compare)) {
      result = true;
    }
  });

  return result;
};

const Capitalize = str => str.charAt(0).toUpperCase() + string.slice(1);

//# sourceMappingURL=Utility.js.map

//# sourceMappingURL=Utility.js.map
