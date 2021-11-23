roleTexts = [];

function getRole(roles, textLine) {
    for (let i = 0; i < roles.length; i++) {
        if (textLine.startsWith(roles[i])) {
            return roles[i];
        }
    }
}

function transform(roles, textLines) {
    for (let i = 0; i < roles.length; i++) {
        this.roleTexts.push({
            role: roles[i],
            lines: []
        });
    }

    for (let i = 0; i < textLines.length; i++) {
        let role = getRole(roles, textLines[i]);
        if (role === undefined) continue;

        this.roleTexts.find((roleText) => roleText.role === role).lines.push({
            lineNumber: i + 1,
            text: textLines[i].substr(role.length, textLines[i].length - role.length)
        });
    }

    outputRoleTexts();
}

function callTransformFunction() {
    let roles = ["Городничий","Аммос Федорович", "Артемий Филиппович","Лука Лукич"];
    let textLines = [
        "Городничий: Я пригласил вас, господа, с тем, чтобы сообщить вам пренеприятное известие: к нам едет ревизор.",
        "Аммос Федорович: Как ревизор?",
        "Артемий Филиппович: Как ревизор?",
        "Городничий: Ревизор из Петербурга, инкогнито. И еще с секретным предписаньем.",
        "Аммос Федорович: Вот те на!",
        "Артемий Филиппович: Вот не было заботы, так подай!",
        "Лука Лукич: Господи боже! еще и с секретным предписаньем!"
    ]

    transform(roles, textLines);
}

function outputRoleTexts() {
    console.log(roleTexts);

    let show = document.getElementById("show");
    this.roleTexts.forEach((roleText) => {
        show.innerText += `${roleText.role}:\n`;
        roleText.lines.forEach((line) => show.innerText += `${line.lineNumber}: ${line.text}\n`)
    });
}