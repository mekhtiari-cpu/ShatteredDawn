// Interface for NPCs
public interface INPC
{
    void Interact(PlayerQuestHandler questHandler);
    void OnQuestAccepted(PlayerQuestHandler questHandler);
    void GiveReward(Quest quest);
    void OnQuestDeclined();
    void UpdateAssignedQuest();
    
    string GetNPCType();

    bool HasQuest();
}
