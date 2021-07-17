const fs = require("fs");
var slowa="about people moon spaceship programming friend passworld wall teacher president language optimist friendship championship station";
var passwords={"passworlds":slowa.split(" ")}

fs.writeFileSync('passwords.json', JSON.stringify(passwords));
console.log("compleate");
