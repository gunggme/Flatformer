package kr.pah.pcs.flatformer_be.dto;

import lombok.Data;
import lombok.Getter;

@Data
@Getter
public class RankingDto {
    private String playerName;
    private double time;

    public RankingDto(String playerName, double time) {
        this.playerName = playerName;
        this.time = time;
    }
}
