const PROCESSED_LABEL_NAME = 'game-processed';

function checkGameEmails() {
  const label = getOrCreateLabel(PROCESSED_LABEL_NAME);

  const query = `in:inbox -label:${PROCESSED_LABEL_NAME}`;
  const threads = GmailApp.search(query, 0, 10);

  for (const thread of threads) {
    const messages = thread.getMessages();
    const lastMessage = messages[messages.length - 1];

    const sender = lastMessage.getFrom();
    const password = generatePasswordForCurrentTime();

    const subject = 'Пароль для уровня';
    const body = [
      'Привет!',
      '',
      `Пароль для уровня: ${password}`,
      '',
      'Пароль меняется каждые 10 минут.'
    ].join('\n');

    GmailApp.sendEmail(sender, subject, body);

    thread.addLabel(label);
  }
}

function generatePasswordForCurrentTime() {
  const now = new Date();

  const year = now.getUTCFullYear();
  const month = now.getUTCMonth() + 1;
  const day = now.getUTCDate();
  const hour = now.getUTCHours();
  const tenMinuteBlock = Math.floor(now.getUTCMinutes() / 10);

  const rawValue =
    year * 13 +
    month * 17 +
    day * 19 +
    hour * 23 +
    tenMinuteBlock * 29;

  const code = rawValue % 100000;

  return String(code).padStart(5, '0');
}

function getOrCreateLabel(labelName) {
  const existingLabel = GmailApp.getUserLabelByName(labelName);

  if (existingLabel != null) {
    return existingLabel;
  }

  return GmailApp.createLabel(labelName);
}