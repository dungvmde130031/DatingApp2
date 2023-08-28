SELECT *
FROM Messages AS m
INNER JOIN Users AS sender 
    ON m.SenderId = sender.Id
INNER JOIN Photos AS senderPhotos 
    ON sender.Id = senderPhotos.AppUserId 
    AND senderPhotos.IsMain = 1
INNER JOIN Users AS recipient 
    ON m.RecipientId = recipient.Id
INNER JOIN Photos AS recipientPhotos 
    ON recipient.Id = recipientPhotos.AppUserId 
    AND recipientPhotos.IsMain = 1
WHERE
    (m.RecipientUsername = 'dungvm' 
        AND m.RecipientDeleted = false 
        AND m.SenderUsername = 'ribi')
    OR (m.RecipientUsername = 'ribi' 
        AND m.SenderDeleted = false 
        AND m.SenderUsername = 'dungvm')
ORDER BY m.MessageSent;

-- var messages = await _context.Messages
--     .Include(u => u.Sender).ThenInclude(p => p.Photos)
--     .Include(u => u.Recipient).ThenInclude(p => p.Photos)
--     .Where(
--         m => m.RecipientUsername == currentUserName && m.RecipientDeleted == false &&
--         m.SenderUsername == recipientUserName ||
--         m.RecipientUsername == recipientUserName && m.SenderDeleted == false &&
--         m.SenderUsername == currentUserName
--     )
--     .OrderBy(m => m.MessageSent)
--     .ToListAsync();

