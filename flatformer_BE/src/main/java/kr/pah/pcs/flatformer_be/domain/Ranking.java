package kr.pah.pcs.flatformer_be.domain;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;

@AllArgsConstructor
@NoArgsConstructor
@Getter
@Entity
public class Ranking {
    @Id @GeneratedValue
    @Column(name = "ranking_id")
    private Long id;
    @Column(unique = true)
    private String playerName;
    private double time;

    public void modified(String playerName, double time) {
        this.playerName = playerName;
        this.time = time;
    }
}
